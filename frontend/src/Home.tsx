import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { ThemeProvider } from "@emotion/react";
import { darkTheme, lightTheme } from "./themes";
import { useSearchBookQuery } from "./api/apiSlice";
import { CircularProgress } from "@mui/material";
import { List } from "@mui/material";
import { BookItem } from "./stories/Book/Book";
import ResponsiveAppBar from "./stories/AppBar/ResponsiveAppBar";
import { lightBlue } from "@mui/material/colors";
import { Navigate } from "react-router-dom";

function Home() {
  const { data, isLoading, isError, error } = useSearchBookQuery();
  if (isLoading) return <CircularProgress />;
  if (isError)
    return <p>Oops, Something went wrong! {JSON.stringify(error)}</p>;

  return (
    //TODO: Hook theming up to redux so that it selects
    <ThemeProvider theme={lightTheme}>
      <ResponsiveAppBar></ResponsiveAppBar>
      <div className="App">
        {data!.length === 0 && <p>No Todo Lists!</p>}
        {data!.length !== 0 && (
          <List>
            {data!.map((book) => (
              <BookItem book={book} key={book.id} />
            ))}
          </List>
        )}
      </div>
    </ThemeProvider>
  );
}

export default Home;
