import { Component, OnInit } from '@angular/core';
import { SubjectModel } from '../models/subjectModel';
import { LoadingService } from 'src/app/services/loading-service/loading.service';
import { SubjectsService } from '../services/subjects.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-subject-form',
  templateUrl: './subject-form.component.html',
  styleUrls: ['./subject-form.component.css']
})
export class SubjectFormComponent implements OnInit {

  constructor(
    private loadingService: LoadingService,
    private subjectsService: SubjectsService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadingService.endLoading();
    this.page.subjectId = +this.activatedRoute.snapshot.paramMap.get("id");
    this.methods.getSubject();
  }

  page = {
    subjectId: 0 as number,
    subject: null as SubjectModel
  }

  methods = {
    getSubject: () => {
      if (this.page.subjectId == 0) {
        this.page.subject = new SubjectModel(0, "", "", "", "", "", "", "");
      }
      else
      {
        this.loadingService.addLoading();
        this.subjectsService.get(this.page.subjectId)
          .then((subject) => {
            this.page.subject = subject;
            this.loadingService.endLoading();
          })
          .catch((error) => {

            this.loadingService.endLoading();
          });
      }
    },
    save: () => {
      this.loadingService.addLoading();
      this.subjectsService.save(this.page.subject)
        .then(() => {
          this.loadingService.endLoading();
          this.methods.back();
        }, (error) => {
          console.log(error);
          this.loadingService.endLoading();
        })
    },
    back: () => {
      this.router.navigate(['subjects']);
    }
  }
}
