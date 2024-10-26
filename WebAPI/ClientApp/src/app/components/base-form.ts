import { Directive, inject, OnDestroy } from "@angular/core";
import { UntypedFormBuilder, UntypedFormGroup } from "@angular/forms";
import { SubSink } from "subsink";
@Directive()
export abstract class BaseFormComponent implements OnDestroy {
  // public single_subject:boolean;
  protected subs: SubSink = new SubSink();
  submitForm: UntypedFormGroup;
  submited: boolean = false;
  saving: boolean = false;
  loading: boolean = false;
  protected formBuilder: UntypedFormBuilder;
  constructor(
    controls: { [key: string]: any, },
    options?: {
      [key: string]: any;
    }
  ) {
    this.formBuilder = inject(UntypedFormBuilder);
    this.submitForm = this.formBuilder.group(controls, options);
  }
  get f() { return this.submitForm.controls; }
  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
  abstract save(): void | Promise<void>;
  onInitData() {

  }
  onSave() {

    this.submited = true;
    if (this.submitForm.valid) {
      this.save();
    } else {
      console.log(this.submitForm.controls);
      Object.values(this.submitForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }

  }
  get formModel() {
    let model: { [key: string]: any } = {};
    Object.keys(this.submitForm.controls).forEach((key, index) => {
      model[key] = this.submitForm.controls[key].value;
    });
    return model;
  }
}
