import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RecipesComponent } from './components/recipes/recipes.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ContactComponent } from './components/contact/contact.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { FaqComponent } from './components/faq/faq.component';

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'recipes', component: RecipesComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  // {path:'category/:category', component: RecipesComponent},
  // {path:'search/:inputText', component: RecipesComponent},
  // {path:'userRecipes/:username', component: RecipesComponent},
  {path:'contact',component: ContactComponent},
  {path:'aboutUs', component: AboutUsComponent},
  {path:'faq', component: FaqComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
