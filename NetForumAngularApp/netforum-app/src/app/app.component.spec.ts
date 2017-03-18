import { TestBed, async } from '@angular/core/testing';

import { RegisterComponent } from './components/app.register.component';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        RegisterComponent
      ],
    }).compileComponents();
  }));

  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(RegisterComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));

  it(`should have as title 'Huehue :D'`, async(() => {
    const fixture = TestBed.createComponent(RegisterComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title + ' ' + app.age).toEqual('Huehue :D');
  }));

  it('should render title in a h1 tag', async(() => {
    const fixture = TestBed.createComponent(RegisterComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent);
  }));

    it('should render title in a p tag', async(() => {
    const fixture = TestBed.createComponent(RegisterComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('p').textContent);
  }));
});
