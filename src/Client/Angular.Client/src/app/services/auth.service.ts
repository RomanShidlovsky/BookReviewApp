import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import signUp from "../models/signUp";
import {identityEndpoints} from "../constants/endpoints";
import signIn from "../models/signIn";
import {firstValueFrom} from "rxjs";
import tokensResponse from "../models/tokensResoponse";
import {client} from "../constants/client";
import {jwtDecode} from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  tokenRefreshing = false;

  constructor(private http: HttpClient) { }

  async register(userCredentials: signUp) {
    return this.http.post(identityEndpoints.register, userCredentials);
  }

  async login(userCredentials: signIn) {
    let body = new URLSearchParams();
    body.set('username', userCredentials.userName);
    body.set('password', userCredentials.password);
    body.set('grant_type', 'password');
    body.set('client_id', client.id);
    body.set('client_secret', client.secret);

    const options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    }

    const tokens = await firstValueFrom(
      this.http.post<tokensResponse>(identityEndpoints.login, body.toString(), options)
    ).catch(() => {});

    if (tokens) {
      this.setSession(tokens);

      return true;
    }

    return false;
  }

  async refreshToken(): Promise<boolean> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      return false;
    }

    this.tokenRefreshing = true;

    let body = new URLSearchParams();
    body.set('refresh_token', refreshToken);
    body.set('grant_type', 'refresh_token');
    body.set('client_id', client.id);
    body.set('client_secret', client.secret);

    const options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    }

    const tokens = await firstValueFrom(
      this.http.post<tokensResponse>(identityEndpoints.login, body.toString(), options)
    ).catch(() => {});

    if (tokens) {
      this.setSession(tokens);
      this.tokenRefreshing = false;

      return true;
    }

    return false;
  }

  logOut() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('expires_in');
  }

  getAccessToken() {
    return localStorage.getItem('access_token');
  }

  getRefreshToken() {
    return localStorage.getItem('refresh_token');
  }

  getExpiresIn() {
    return localStorage.getItem('expires_in');
  }

  isLogged() {
    const expiration = this.getExpiresIn();

    if (expiration) {
      const expiresIn = new Date(JSON.parse(expiration));
      const currentTime = new Date(Date.now());

      return currentTime < expiresIn;
    }

    return false;
  }

  getUserId(){
    if (!this.isLogged()) {
      return null;
    }

    const accessToken = this.getAccessToken();

    if (accessToken) {
      const payload = jwtDecode(accessToken);

      if (payload.sub) {
        return parseInt(payload.sub);
      } else {
        return null;
      }
    }

    return null;
  }

  setSession(tokens: tokensResponse) {
    const expiresIn = Date.now() + tokens.expires_in * 1000;

    localStorage.setItem('access_token', tokens.access_token);
    localStorage.setItem('refresh_token', tokens.refresh_token);
    localStorage.setItem('expires_in', JSON.stringify(expiresIn));
  }
}
