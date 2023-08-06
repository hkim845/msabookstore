interface Book {
  id: string;
  kind: string;
  selfLink?: string;
  volumeInfo: VolumeInfo;
}

interface VolumeInfo {
  authors: string[];
  title: string;
}

export type { Book, VolumeInfo };
