CREATE TABLE [dbo].[NoteComments]
(
    [Id]               [uniqueidentifier] NOT NULL,
    [CreatedAt]        [datetime]         NOT NULL,
    [NoteId]           [uniqueidentifier] NOT NULL,
    [AuthorId]         [uniqueidentifier] NOT NULL,
    [ReplyToCommentId] [uniqueidentifier] NULL,
    [Content]          [nvarchar](512)    NOT NULL,
    [IsDeleted]        [bit]              NOT NULL
) ON [PRIMARY]
GO