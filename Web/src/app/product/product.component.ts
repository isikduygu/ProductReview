import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal, ModalDismissReasons, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IReview } from '../models/IReview';
import { ReviewService } from '../services/review.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: [`
  .star {
    position: relative;
    display: inline-block;
    font-size: 3rem;
    color: #d3d3d3;
  }
  .full {
    color: red;
  }
  .half {
    position: absolute;
    display: inline-block;
    overflow: hidden;
    color: red;
  }
  .box{
    box-shadow: rgba(0, 0, 0, 0.05) 0px 6px 24px 0px, rgba(0, 0, 0, 0.08) 0px 0px 0px 1px;
    padding: 10px 20px 10px 20px;
    margin: 10px;
    width: 50%;
    border-radius: 8px;
  }
  .box img{
    margin-right: 10px;
    margin-bottom: 10px;
  }
`]

})
export class ProductComponent implements OnInit {

  constructor(private reviewService : ReviewService,private modalService: NgbModal ) { }
  closeResult = '';
  currentRate = 0;
  submitted = false;

  ngOnInit(): void {
    this.getReview();
  }
  Review : IReview ={
    name: "",
    lastname: "",
    comment: "",
    rate: 0,
  }
  getAllReview : IReview [] = [];

  reviewForm= new FormGroup({
    name:new FormControl(""),
    lastname: new FormControl(""),
    comment: new FormControl(""),
    rate: new FormControl(0),
  });

  open(content: any) {
    this.reviewService.getAllPReviews()
    .subscribe(() => {
    });
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  send(currentRate : number, modal: NgbActiveModal){
    this.Review.name = this.reviewForm.value.name as string,
    this.Review.lastname = this.reviewForm.value.lastname as string,
    this.Review.comment = this.reviewForm.value.comment as string;
    this.Review.rate = this.currentRate;
      this.reviewService
      .createReview(this.Review)
      .subscribe(() => {
        this.getReview();
        modal.dismiss();
      });
  }
  getReview(){
      this.reviewService
      .getAllPReviews()
      .subscribe({
        next: (response : any) => {
          this.getAllReview = response;
        }
      });
  }
  get f(): { [key: string]: AbstractControl } {
    return this.reviewForm.controls;
  }
}
