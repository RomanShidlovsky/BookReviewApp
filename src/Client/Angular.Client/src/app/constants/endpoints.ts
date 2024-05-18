export const gateway = "https://localhost:5000";

export const identityEndpoints = {
  login: `${gateway}/authentication/login`,
  register: `${gateway}/authentication/register`
}

export const bookEndpoints = {
  books: `${gateway}/books`
}

export const reviewEndpoints = {
  likes(reviewId: string) {
    return `${gateway}/reviews/${reviewId}/likes`;
  },
  dislikes(reviewId: string) {
    return `${gateway}/reviews/${reviewId}/dislikes`;
  }
}
