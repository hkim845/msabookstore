import React, { useEffect, useState } from "react";
import logo from "./logo.svg";
import "./App.css";
import { ThemeProvider } from "@emotion/react";
import { darkTheme, lightTheme } from "./themes";
import { useLazySearchBookQuery, useSearchBookQuery } from "./api/apiSlice";
import { Button, CircularProgress, TextField } from "@mui/material";
import { List } from "@mui/material";
import { BookItem } from "./stories/Book/Book";
import ResponsiveAppBar from "./stories/AppBar/ResponsiveAppBar";
import { Book } from "./models/Book";

function Home() {
  const booksArr: Book[] = [];
  const [trigger, { data }] = useLazySearchBookQuery();
  const [books, setBooks] = useState(booksArr);
  const [title, setTitle] = useState("");

  const handleClick = () => {
    trigger(`title=${title}`);
  };

  const handleChange = (event: any) => {
    setTitle(event.target.value);
  };

  useEffect(() => {
    if (data != null) {
      setBooks(data);
    }
  }, [data]);

  return (
    //TODO: Hook theming up to redux so that it selects
    <ThemeProvider theme={lightTheme}>
      <ResponsiveAppBar></ResponsiveAppBar>
      <div className="App">
        <div className="input-container">
          <TextField
            id="book-title"
            label="Book Title"
            variant="outlined"
            sx={{ marginRight: "1rem" }}
            onChange={handleChange}
          />
          <Button variant="contained" onClick={handleClick}>
            Search
          </Button>
        </div>
        <div className="book-container">
          {books!.length !== 0 && (
            <List>
              {books!.map((book) => (
                <BookItem book={book} key={book.id} />
              ))}
            </List>
          )}
        </div>
      </div>
    </ThemeProvider>
  );
}

export default Home;
