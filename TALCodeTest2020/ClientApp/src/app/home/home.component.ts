import { Component, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import * as moment from 'moment';
import 'moment/locale/en-au';

@Component({
  selector: 'ap-home',
  templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
  public premium = 0;
  public quoteForm: FormGroup;
  public loadingError = false;
  private submitted = false;

  public occupationRatings: OccupationRatingModel[];

  @ViewChild("fullName", { static: false }) fullNameField: ElementRef;
  @ViewChild("sumInsured", { static: false }) sumInsuredField: ElementRef;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseApiUrl: string,
    private formBuilder: FormBuilder,
    private el: ElementRef) {
  }

  ngOnInit() {
    this.loadingError = false;
    this.quoteForm = this.formBuilder.group({
      name: ['', Validators.required],
      age: ['', Validators.required],
      dob: ['', Validators.required],
      occupationRating: ['', Validators.required],
      amount: ['', Validators.required],
    });

    this.getOccupationRatings();
  }

  ngAfterViewInit() { this.fullNameField.nativeElement.focus(); }

  get f() { return this.quoteForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.quoteForm.invalid) {
      return;
    }

    this.calcPremium();
  }

  onReset() {
    this.loadingError = false;
    this.submitted = false;
    this.quoteForm.reset();
    this.premium = 0;
  }

  onOccupationChange(e) {
    this.sumInsuredField.nativeElement.focus();
    this.premium = 0;
    this.onSubmit();
  }

  onDobChange(e) {
    this.calcAge(e.target.value);
  }

  displayError(error) {
    console.error(error)
    this.loadingError = true;
  }

  getOccupationRatings() {
    this.http.get<OccupationRatingModel[]>(this.baseApiUrl + 'premium')
      .subscribe(result => {
        this.occupationRatings = result;
      }, error => this.displayError(error));
  }

  calcPremium() {
    let params = new HttpParams();
    params = params.append('amount', this.quoteForm.get('amount').value);
    params = params.append('occupationRating', this.quoteForm.get('occupationRating').value);
    params = params.append('age', this.quoteForm.get('age').value);
    params = params.append('dob', this.quoteForm.get('dob').value);

    this.http.get<PremiumQuoteModel>(this.baseApiUrl + 'premium/quote', { params: params })
      .subscribe(result => {
        this.premium = result.premium;
        this.setAge(result.age);
      }, error => this.displayError(error));
  }

  calcAge(dob) {
    if (!dob)
      return;
    let age = moment().diff(moment(dob, 'dd/MM/yyyy'), 'years');
    if (!isNaN(age))
      this.quoteForm.get('age').setValue(age);
    else
      this.quoteForm.get('age').setValue('');

    this.quoteForm.controls['age'].enable();
  }

  setAge(age) {
    if (this.quoteForm.get('age').value != age)
      this.quoteForm.get('age').setValue(age);
  }
}

interface OccupationRatingModel {
  occupation: string;
  rating: number;
}

interface PremiumQuoteModel {
  premium: number;
  amount: number;
  occupationRating: number;
  age: number;
  dob: string;
}