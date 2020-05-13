import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Items } from 'src/app/Models/items';
import { ItemsService } from 'src/app/services/items.service';
import { Router } from '@angular/router';
import { Category } from 'src/app/Models/category';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private builder:FormBuilder,private service:ItemsService,private route:Router) { }

  ngOnInit() {
   
  }
}
