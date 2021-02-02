import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  constructor(
    private snackBar: MatSnackBar,
    private router: Router,
    private http: HttpClient,
    public breakpointObserver: BreakpointObserver
  ) { }

  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  private readonly url = "http://localhost:27811/api/Products";
  public loading = false;
  mobile = false;

  product: any = {
    id: 0,
    name: null,
    price: null,
    image: null
  }

  data: any = [];

  productFormControl: FormControl = new FormControl('', [
    Validators.required,
    Validators.nullValidator
  ]);

  priceFormControl: FormControl = new FormControl('', [
    Validators.required,
    Validators.nullValidator
  ]);

  imageFormControl: FormControl = new FormControl('', [
  ]);

  showSnackBar(item, error = false) {
    this.snackBar.open(!error ? item : ':(', error ? item : ':)', {
      duration: 3000,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
    });
  }

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is completed
        this.product.image = event.target.result;
      }
    }
  }

  cancel() {
    this.clear();
    this.getList();
  }

  clear() {
    this.productFormControl.reset();
    this.priceFormControl.reset();
    this.product.id = 0;
    this.product.name = "";
    this.product.price = "";
    this.product.image = null;
  }

  getList() {
    this.loading = true;
    this.http.get<any>(this.url).
      subscribe(
        response => {
          if (response.success == true) {
            this.data = response.response;
          }
          else {
            this.showSnackBar(response.error);
          }
          this.loading = false;
        },
        error => {
          this.loading = false;
          console.log('error', error);
        });
  }

  delete(item) {
    if (item.id <= 0)
      return;

    this.loading = true;

    this.http.delete<any>(`${this.url}/${item.id}`).
      subscribe(
        response => {
          if (response.success == true) {
            this.getList();
            this.showSnackBar(response.response);
          }
          else {
            this.showSnackBar(response.error);
          }
          this.loading = false;
        },
        error => {
          this.loading = false;
          console.log('error', error);
        });
  }

  onSubmit() {
    if (this.product.name == null || this.product.price == null)
      return;
    else if (this.product.id == 0)
      this.save();
    else
      this.update();
  }

  save() {
    this.loading = true;
    this.http.post<any>(this.url, this.product).
      subscribe(
        response => {
          if (response.success == true) {
            this.clear();
            this.getList();
            this.showSnackBar(response.response);
          }
          else {
            this.showSnackBar(response.error);
          }
          this.loading = false;
        },
        error => {
          this.loading = false;
          console.log('error', error);
        });
  }

  update() {
    this.loading = true;
    this.http.put<any>(this.url, this.product).
      subscribe(
        response => {
          if (response.success == true) {
            this.clear();
            this.getList();
            this.showSnackBar(response.response);
          }
          else {
            this.showSnackBar(response.error);
          }
          this.loading = false;
        },
        error => {
          this.loading = false;
          console.log('error', error);
        });
  }

  edit(item) {
    this.product.id = item.id;
    this.product.name = item.name;
    this.product.price = item.price;
    this.product.image = item.image;
    this.data.splice(this.data.indexOf(item), 1);
  }

  logout() {
    this.router.navigate(['/login']);
  }

  ngOnInit(): void {
    this.getList();

    //a tabela quebra se usar o modo de ocultar do próprio bootstrap, então tive que "escutar" o screen para resolver
    this.breakpointObserver.observe([
      '(max-width: 768px)'
    ]).subscribe(result => {
      if (result.matches) {
        this.mobile = true;
      } else {
        this.mobile = false;
      }
    });
  }
}
