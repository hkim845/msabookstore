import { List, ListItem } from "@mui/material";
import { Book } from "../../models/Book";
import { BookCard } from "../BookCard/BookCard";

export interface BookProps {
  book: Book;
}

const BookItem = ({ book }: BookProps) => {
  return (
    <ListItem key={book.id}>
      <BookCard {...book} />
    </ListItem>
  );
};

export { BookItem };
