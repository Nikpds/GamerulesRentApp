import { Injectable } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root'
})
export class NotifyService {

  constructor(private toastr: ToastrService) {
    this.toastr.toastrConfig.progressBar = true;
    this.toastr.toastrConfig.progressAnimation = 'decreasing';

  }

  success(message: string = 'To αίτημα σας πραγματοποιήθηκε με επιτυχία!', title: string = 'Επιτυχία') {
    this.toastr.success(message, title);
  }

  warning(message, title: string = 'Προσοχή') {
    this.toastr.warning(message, title);
  }

  error(message: string = 'To αίτημα απέτυχε.', title: string = 'Σφάλμα') {
    this.toastr.error(message, title);
  }

  info(message, title: string = 'Ενημέρωση') {
    this.toastr.info(message, title);
  }
}
