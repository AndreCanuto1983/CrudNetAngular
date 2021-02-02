import { Component, OnInit } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private snackBar: MatSnackBar,
    private router: Router,
    private http: HttpClient
  ) { }

  private readonly url = "https://dev.sitemercado.com.br/api/login";
  public loading = false;

  userData: any = {
    nome: '',
    senha: ''
  }

  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  nameFormControl = new FormControl('', [
    Validators.required,
    Validators.nullValidator
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.nullValidator
  ]);

  showSnackBar(item, error = false) {
    this.snackBar.open(!error ? item : ':(', error ? item : ':)', {
      duration: 3000,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
    });
  }

  onSubmit() {
    if (this.userData.nome == "" || this.userData.senha == "")
      return;
    // remova o return e coloque isto para pular a verificação e testar direto na tela de produtos => this.router.navigate(['/products']);
    // fiz isso só pq é apenas um teste, jamais faça em sistemas profissionais
    else
      this.login();
  }

  login() {
    this.loading = true;
    this.http.post<any>(this.url, this.userData).
      subscribe(
        response => {
          if (response.success == true)
            this.router.navigate(['/products']);
          else
            this.showSnackBar(response.error, true);
          this.loading = false;
        },
        error => {
          this.loading = false;
          console.log('error', error);
        });
  }

  ngOnInit(): void {
  }
}
