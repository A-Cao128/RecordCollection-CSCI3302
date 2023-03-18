CREATE TABLE [dbo].[AlbumGenre] (
    [AlbumID] INT NOT NULL,
    [GenreID] INT NOT NULL,
    CONSTRAINT [FK_AlbumGenre_Album] FOREIGN KEY ([AlbumID]) REFERENCES [dbo].[Album] ([AlbumID]),
    CONSTRAINT [FK_AlbumGenre_Genre] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genre] ([GenreID])
);

