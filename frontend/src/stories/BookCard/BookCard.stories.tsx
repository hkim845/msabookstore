import type { Meta, StoryObj } from "@storybook/react";

import { BookCard } from "./BookCard";
import { within, userEvent } from "@storybook/testing-library";

// More on how to set up stories at: https://storybook.js.org/docs/react/writing-stories/introduction
const meta = {
  title: "Components/Todo Card",
  component: BookCard,
  tags: ["autodocs"],
} satisfies Meta<typeof BookCard>;

export default meta;
type Story = StoryObj<typeof meta>;

// More on writing stories with args: https://storybook.js.org/docs/react/writing-stories/args
export const BasicTodo: Story = {
  args: {
    id: "1",
    kind: "volume",
    selfLink: "selfLink",
    volumeInfo: { title: "Brave New World", authors: ["Aldous Huxley"] },
  },
};

export const TodoWithItem: Story = {
  args: {
    id: "2",
    kind: "volume",
    selfLink: "selfLink",
    volumeInfo: { title: "Animal Farm", authors: ["George Owell"] },
  },
  play: async ({ args, canvasElement, step }) => {
    const canvas = within(canvasElement);

    await step("Click Share", async () => {
      await userEvent.click(canvas.getByTestId("todo-card-share"));
    });
    await step("Click Done", async () => {
      await userEvent.click(canvas.getByTestId("todo-card-done"));
    });
    await step("Click Close", async () => {
      await userEvent.click(canvas.getByTestId("todo-card-close"));
    });
  },
};
