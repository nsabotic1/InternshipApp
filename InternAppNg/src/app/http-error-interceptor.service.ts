import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { catchError, Observable, of, retry, throwError } from "rxjs";


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private snackBar: MatSnackBar) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let handled: boolean = false;

    return next.handle(request).pipe(
      retry(1),
      catchError((error) => {
        let errorMessage = null;

        if (error.error instanceof ErrorEvent)
        { //client side errors
          errorMessage = 'Error: ' + error.error.message;
          this.snackBar.open(errorMessage, 'Close', { duration: 3000 })
        }
        else if (error instanceof HttpErrorResponse)
        { //server side errors
          errorMessage = 'Error status: ' + error.error.status;
          this.snackBar.open(errorMessage, 'Close', { duration: 3000 })
          handled = this.handleServerSide(error);
        }

        if (!handled) {
          if (errorMessage)
          {
            this.snackBar.open(errorMessage, 'Close', { duration: 3000 })
            return throwError(() => {
              new Error(errorMessage);
            })
          }
          else
          {
            this.snackBar.open('Unexpected error occured', 'Close', { duration: 3000 })
            return throwError(() => {
              new Error('Unexpected error occured');
            })
          }
        }
        else
        {
          return of(error);
        }
      })
    )
  }

  private handleServerSide(error: HttpErrorResponse): boolean {
    let handled: boolean = false;

    switch (error.status) {
      case 401:
        this.snackBar.open('Please log in again.', 'Close', { duration: 3000 }) //unauthorized
        handled = true;
        break;

      case 403:
        this.snackBar.open('You Are not authorized.', 'Close', { duration: 3000 }) //forbidden
        handled = true;
        break;

      case 400:
        this.snackBar.open('Form not filled correctly!', 'Close', { duration: 3000 }) //bad request
        handled = true;
        break;

      case 500:
        console.log(error)
        this.snackBar.open(error.error.message, 'Close', { duration: 3000 }) //internal server error
        handled = true;
        break;

      default:
        this.snackBar.open('Unknown server error', 'Close', { duration: 3000 })
        handled = true;
        break;
    }

    return handled;
  }
}
