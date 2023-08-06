import { Close, Done, Share } from "@mui/icons-material";
import {
  Card,
  CardActions,
  CardContent,
  CardHeader,
  IconButton,
  styled,
} from "@mui/material";
import React from "react";
import { VolumeInfo } from "../../models/Book";

export interface BookProps {
  id: string;
  kind: string;
  selfLink?: string;
  volumeInfo: VolumeInfo;
}

const StyledCard = styled(Card)(({ theme }) => ({
  maxWidth: "500px",
}));

const BookCard = ({ id, kind, selfLink, volumeInfo }: BookProps) => {
  return (
    <StyledCard>
      <CardHeader
        title={volumeInfo.title}
        subheader={`Created By: ${volumeInfo.authors[0]}`}
      />
      <CardContent>{volumeInfo.title}</CardContent>
      <CardActions
        sx={{
          alignContent: "flex-end",
          justifyContent: "end",
          alignSelf: "end",
        }}
      >
        {/*TODO: Set up these actions correctly */}
        <IconButton>
          <Share data-testid="todo-card-share" />
        </IconButton>
        <IconButton>
          <Done data-testid="todo-card-done" />
        </IconButton>
        <IconButton>
          <Close data-testid="todo-card-close" />
        </IconButton>
      </CardActions>
    </StyledCard>
  );
};

export { BookCard };
