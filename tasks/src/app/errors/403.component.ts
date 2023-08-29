import { Component } from '@angular/core'

@Component({
  template: `
    <h1 class="errorMessage">This page is not available to you.</h1>
  `,
  styles: [`
    .errorMessage { 
      text-align: center; 
    }`]
})
export class Error403Component{
  constructor() {

  }

}