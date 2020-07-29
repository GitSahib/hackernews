import {Directive, HostListener} from "@angular/core";

@Directive({
    selector: "[stop-propagation]"
})
export class StopPropagation
{
  @HostListener("click", ["$event"])
  public onClick(event: any): boolean
    {
      event.stopPropagation();
      return false;
    }
}
