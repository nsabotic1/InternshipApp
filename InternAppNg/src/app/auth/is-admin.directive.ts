import { Directive, OnChanges, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from './auth.service';

@Directive({
  selector: '[appIsAdmin]'
})
export class IsAdminDirective implements OnChanges{
  isAdmin: boolean;

  constructor(private auth: AuthService, private vcr: ViewContainerRef, private tempRef: TemplateRef<any>) { }

  ngOnInit() {
    this.isAdmin = this.auth.hasAccess();
  }

  ngOnChanges () {
    this.isAdmin ? this.vcr.createEmbeddedView(this.tempRef) : this.vcr.clear();
  }
}
