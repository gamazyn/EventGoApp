import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss'],
})
export class TitleComponent {
  @Input() title: string = '';

  @Input() subtitle: string = 'Desde 2022';

  @Input() iconClass: string = 'fa fa-user';

  @Input() listButton: boolean = false;

  constructor(private router: Router) {}

  list() {
    this.router.navigate([`/${this.title.toLocaleLowerCase()}/lista`]);
  }
}
