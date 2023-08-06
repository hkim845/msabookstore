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
    // Todo item endpoints
    getTodoItemById: builder.query({
      query: (id) => `TodoItem/${id}`,
    }),
    searchBook: builder.query<Book[], void>({
      query: () => ({
        url: "Book",
        method: "GET",
      }),
    }),
    updateTodoItem: builder.mutation({
      query: (todoItem) => ({
        url: `TodoItem/${todoItem.Id}`,
        method: "PUT",
        body: todoItem,
      }),
    }),
    login: builder.mutation({
      query: (user: User) => ({
        url: "User/login",
        method: "POST",
        body: user,
      }),
    }),
    deleteTodoItem: builder.mutation({
      query: (id) => ({
        url: `TodoItem/${id}`,
        method: "DELETE",
      }),
    }),

    getTodoListById: builder.query({
      query: (id) => `TodoList/${id}`,
    }),
    updateTodoList: builder.mutation({
      query: (todoList) => ({
        url: `TodoList/${todoList.Id}`,
        method: "PUT",
        body: todoList,
      }),
    }),
    createTodoList: builder.mutation({
      query: (todoList) => ({
        url: "TodoList",
        method: "POST",
        body: todoList,
      }),
    }),
    deleteTodoList: builder.mutation({
      query: (id) => ({
        url: `TodoList/${id}`,
        method: "DELETE",
      }),
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const {
  useSearchBookQuery,
  useGetTodoItemByIdQuery,
  useUpdateTodoItemMutation,
  useLoginMutation,
  useDeleteTodoItemMutation,
  useGetTodoListByIdQuery,
  useUpdateTodoListMutation,
  useCreateTodoListMutation,
  useDeleteTodoListMutation,
} = api;
