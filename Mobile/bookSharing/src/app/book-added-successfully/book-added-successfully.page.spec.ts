import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { BookAddedSuccessfullyPage } from './book-added-successfully.page';

describe('BookAddedSuccessfullyPage', () => {
  let component: BookAddedSuccessfullyPage;
  let fixture: ComponentFixture<BookAddedSuccessfullyPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ BookAddedSuccessfullyPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(BookAddedSuccessfullyPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
