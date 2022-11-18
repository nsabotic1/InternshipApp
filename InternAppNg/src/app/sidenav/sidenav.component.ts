import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: 'sidenav.component.html',
  styleUrls: ['sidenav.component.css'],
})
export class SidenavComponent implements OnInit {
  selectedButton: number = 1;
  mobileQuery: MediaQueryList;
  isAdmin: boolean;

  private _mobileQueryListener: () => void;

  constructor(changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    private authService: AuthService,
    private router: Router) {
    this.mobileQuery = media.matchMedia('(max-width: 768px)'); //gets current media size and matches it agains 768 px
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
  }

  ngOnInit(): void {
    this.isAdmin = this.authService.hasAccess() //checks if current user is admin
  }

  onLogout() {
    this.authService.logOut();
    this.router.navigate(['/login'])
  }

  onSelect(button) {
    this.selectedButton = button;
  }
}
