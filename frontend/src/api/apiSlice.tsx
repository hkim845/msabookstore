// Need to use the React-specific entry point to import createApi
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Book } from "../models/Book";
import { User } from "../models/User";

const getAuthToken = () => {
  return localStorage.getItem("token");
};

// Define a service using a base URL and expected endpoints
export const api = createApi({
  reducerPath: "api",
  baseQuery: fetchBaseQuery({
    baseUrl: process.env.REACT_APP_API_BASE_URL,
    prepareHeaders: (headers) => {
      const authToken = getAuthToken();
      if (authToken) {
        headers.set("Authorization", `Bearer ${authToken}`);
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    searchBook: builder.query<Book[], string>({
      query: (bookDetails) => `Book/${bookDetails}`,
    }),
    login: builder.mutation({
      query: (user: User) => ({
        url: "User/login",
        method: "POST",
        body: user,
      }),
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useSearchBookQuery, useLazySearchBookQuery, useLoginMutation } =
  api;
