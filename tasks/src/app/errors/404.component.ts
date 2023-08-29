import { Component } from '@angular/core'

@Component({
  template: `
    <h1 class="errorMessage">The page not found.</h1>
  `,
  styles: [`
    .errorMessage { 
      text-align: center; 
    }`]
})
export class Error404Component{
  constructor() {

  }

}