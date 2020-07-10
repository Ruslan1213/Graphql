using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraphQlLibary.DAL.Context;
using GraphQlLibary.Domain.Models;
using Microsoft.AspNetCore.Http;
using GraphQlLibary.Domain.Interfaces.Services;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GraphQlLibary.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly LibaryContext _context;

        private readonly IPhotoService _photoService;

        public PostsController(LibaryContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var libaryContext = _context.Posts.Include(p => p.User).Where(x => x.UserId == GetUserId());

            return View(await libaryContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        public async Task<long> LikeIt([FromQuery]int id)
        {
            var post = await _context.Posts
               .Include(p => p.User)
               .FirstOrDefaultAsync(m => m.Id == id);
            post.Likes += 1;
            _context.Update(post);
            _context.SaveChanges();

            if (post == null)
            {
                return -1;
            }

            return post.Likes;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, IFormFile postPhoto)
        {
            if (ModelState.IsValid && postPhoto != null)
            {
                post.PhotoUri = _photoService.CreatePhoto(postPhoto, post);
                post.DateOfPost = DateTime.Now;
                post.UserId = GetUserId();
                _context.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private int GetUserId()
        {
            int id;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out id);

            return id;
        }
    }
}
