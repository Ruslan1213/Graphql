import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostDetails } from './postDetails.component';

describe('PostDetailsComponent', () => {
  let component: PostDetails;
  let fixture: ComponentFixture<PostDetails>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PostDetails ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
