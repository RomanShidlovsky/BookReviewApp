export const gateway = "https://localhost:5000";

export const identityEndpoints = {
  login: `${gateway}/authentication/login`,
  register: `${gateway}/authentication/register`
}

export const bookEndpoints = {
  pagedBooks: `${gateway}/books`
}
