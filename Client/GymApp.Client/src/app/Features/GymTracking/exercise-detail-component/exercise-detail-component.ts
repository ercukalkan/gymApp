import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { ExerciseService } from '../../../Core/Services/exercise-service';

@Component({
  selector: 'app-exercise-detail-component',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './exercise-detail-component.html',
  styleUrl: './exercise-detail-component.css',
})
  
export class ExerciseDetailComponent implements OnInit {
  private exerciseService = inject(ExerciseService);
  private route = inject(ActivatedRoute);
  exercise?: any;
  cdr = inject(ChangeDetectorRef);
  mode: 'add' | 'edit' = 'edit';
  private router = inject(Router);
  isDeleting = false;

  // Form group for food details
  exerciseForm = new FormGroup({
    // Form controls for editing food details
    nameCtrl: new FormControl(this.exercise?.name || ''),
    descriptionCtrl: new FormControl(this.exercise?.description || ''),
  });

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.mode = id ? 'edit' : 'add';

    if (this.mode === 'edit') {
      this.loadExerciseDetails(id);
    } else {
      // Initialize form for adding new food
      this.exerciseForm.reset({
        nameCtrl: '',
        descriptionCtrl: ''
      });
    }
  }

  onSubmit() {
    let exercise = {
      id: this.exercise?.id,
      name: this.exerciseForm.value.nameCtrl,
      description: this.exerciseForm.value.descriptionCtrl,
    };
    
    if (this.mode === 'add') {
      this.createExercise(exercise);
    } else if (this.mode === 'edit' && this.exercise) {
      this.updateExercise(this.exercise.id, exercise);
    }
  }

  // Public method to be called from template
  onDelete(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.deleteExercise(id);
    }
  }

  private createExercise(exercise: any) {
    this.exerciseService.addExercise(exercise).subscribe(response => {
      this.routerRedirect();
    });
  }

  private updateExercise(id: any, exercise: any) {
    this.exerciseService.updateExercise(id, exercise).subscribe(response => {
      this.routerRedirect();
    });
  }

  private deleteExercise(id: any) {
    // Confirmation dialog
    if (!confirm(`Are you sure you want to delete "${this.exercise?.name}"? This action cannot be undone.`)) {
      return;
    }

    this.isDeleting = true;
    this.exerciseService.deleteExercise(id).subscribe({
      next: () => {
        this.isDeleting = false;
        this.routerRedirect();
      },
      error: (err) => {
        this.isDeleting = false;
        console.error('Error deleting exercise:', err);
        alert('Failed to delete exercise. Please try again.');
      }
    });
  }

  private loadExerciseDetails(id: any) {
    this.exerciseService.getExerciseById(id).subscribe(data => {
        this.exercise = data;
        this.exerciseForm.patchValue({
          nameCtrl: data.name || '',
          descriptionCtrl: data.description || ''
        });
      this.cdr.detectChanges();
    });
  }

  private routerRedirect() {
    this.router.navigate(['/gym-tracking/exercise']);
  }
}