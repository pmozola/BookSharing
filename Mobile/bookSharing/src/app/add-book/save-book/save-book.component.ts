import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLibraryService } from '../services/user-library.service';

@Component({
  selector: 'app-save-book',
  templateUrl: './save-book.component.html',
  styleUrls: ['./save-book.component.scss'],
})
export class SaveBookComponent implements OnInit {
  @Input() isbn: number;
  form: FormGroup;

  constructor(private fb: FormBuilder, private userLibraryService: UserLibraryService, private router: Router) {
  }

  ngOnInit() {
    this.form = this.fb.group({
      isbn: this.isbn,
      rank: [1, Validators.required],
      description: ['']
    });
  }

  save() {
    if (this.form.valid) {
      this.userLibraryService.add(this.form.value).subscribe(_ =>
        this.router.navigate(['bookAdded']));
    }
  }
}
