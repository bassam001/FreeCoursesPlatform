import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoursesService, Course } from '../../services/courses';

@Component({
  standalone: true,
  selector: 'app-courses',
  templateUrl: './courses.html',
  styleUrls: ['./courses.scss'],
  imports: [CommonModule]
})
export class CoursesComponent implements OnInit {

  courses: Course[] = [];

  constructor(private coursesService: CoursesService) { }

  ngOnInit(): void {
    this.coursesService.getAll().subscribe({
      next: (data: Course[]) => {
        this.courses = data;
      },
      error: (err: any) => {
        console.error(err);
      }
    });
  }
}
