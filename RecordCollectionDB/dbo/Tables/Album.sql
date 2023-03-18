CREATE TABLE [dbo].[Album] (
    [AlbumID]            INT           IDENTITY (1, 1) NOT NULL,
    [AlbumTitle]         VARCHAR (200) NOT NULL,
    [ArtistID]           INT           NOT NULL,
    [NumberOfTracks]     INT           NOT NULL,
    [ReleaseDate]        DATETIME      NOT NULL,
    [Price]              DECIMAL (18)  NOT NULL,
    [AlbumCoverImageURL] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED ([AlbumID] ASC),
    CONSTRAINT [ArtistAlbum] FOREIGN KEY ([ArtistID]) REFERENCES [dbo].[Artist] ([ArtistID])
);

