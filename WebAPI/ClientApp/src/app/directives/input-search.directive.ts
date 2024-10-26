import { AfterViewInit, Directive, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { debounceTime, distinctUntilChanged, fromEvent, Subject, switchMap } from 'rxjs';

@Directive({
  selector: '[appInputSearch]',
  standalone: true,
})
export class InputSearchDirective implements AfterViewInit {
  //@Input() initialValue: string = "";
  @Input() debounceTime = 300;
  @Output() textChange = new EventEmitter<string>();
  //@Input() placeholder: string = '';
  inputValue = new Subject<string>();
  trigger = this.inputValue.pipe(
    debounceTime(this.debounceTime),
    distinctUntilChanged()
  );
  constructor(private elementRef: ElementRef) { }

  /**
   *
   */
  ngAfterViewInit(): void {
    fromEvent(this.elementRef.nativeElement, 'keyup')
      .pipe(debounceTime(1000))
      .subscribe(() => {
        this.textChange.emit(this.elementRef.nativeElement.value);
      });
  }

  // @HostListener('input', ['$event'])
  // onInput($event: any) {
  //   this.inputValue.next($event.target.value);
  //   this.textChange.emit($event.target.value);
  // }
}
