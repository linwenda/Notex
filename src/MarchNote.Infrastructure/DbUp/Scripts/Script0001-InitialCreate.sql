/****** Object:  Table [dbo].[EventStore]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventStore]
(
    [EntityId]      [uniqueidentifier] NOT NULL,
    [EntityVersion] [int]              NOT NULL,
    [Timestamp]     [datetime]         NOT NULL,
    [Type]          [nvarchar](max)    NOT NULL,
    [Data]          [nvarchar](max)    NOT NULL,
    CONSTRAINT [EventStore_pk] PRIMARY KEY NONCLUSTERED
        (
         [EntityId] ASC,
         [EntityVersion] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteComments]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteComments]
(
    [Id]               [uniqueidentifier] NOT NULL,
    [CreatedAt]        [datetime]         NOT NULL,
    [NoteId]           [uniqueidentifier] NOT NULL,
    [AuthorId]         [uniqueidentifier] NOT NULL,
    [ReplyToCommentId] [uniqueidentifier] NULL,
    [Content]          [nvarchar](512)    NOT NULL,
    [IsDeleted]        [bit]              NOT NULL,
    CONSTRAINT [PK_NoteComments] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCooperations]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCooperations]
(
    [Id]           [uniqueidentifier] NOT NULL,
    [NoteId]       [uniqueidentifier] NOT NULL,
    [SubmitterId]  [uniqueidentifier] NOT NULL,
    [SubmittedAt]  [datetime]         NOT NULL,
    [AuditorId]    [uniqueidentifier] NULL,
    [AuditedAt]    [datetime]         NULL,
    [Comment]      [nvarchar](256)    NOT NULL,
    [RejectReason] [nvarchar](256)    NULL,
    [Status]       [int]              NOT NULL,
    CONSTRAINT [PK_Cooperations] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteHistories]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteHistories]
(
    [Id]        [uniqueidentifier] NOT NULL,
    [NoteId]    [uniqueidentifier] NULL,
    [AuthorId]  [uniqueidentifier] NULL,
    [AuditedAt] [datetime]         NULL,
    [Title]     [nvarchar](128)    NULL,
    [Content]   [nvarchar](max)    NULL,
    [Version]   [int]              NULL,
    [Comment]   [nvarchar](256)    NULL,
    CONSTRAINT [PK_NoteHistories] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteMembers]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteMembers]
(
    [NoteId]   [uniqueidentifier] NOT NULL,
    [MemberId] [uniqueidentifier] NOT NULL,
    [Role]     [nvarchar](50)     NOT NULL,
    [JoinedAt] [datetime]         NOT NULL,
    [IsActive] [bit]              NOT NULL,
    [LeaveAt]  [datetime]         NULL,
    CONSTRAINT [PK_NoteMembers] PRIMARY KEY CLUSTERED
        (
         [NoteId] ASC,
         [MemberId] ASC,
         [JoinedAt] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes]
(
    [Id]        [uniqueidentifier] NOT NULL,
    [FromId]    [uniqueidentifier] NULL,
    [AuthorId]  [uniqueidentifier] NOT NULL,
    [SpaceId]   [uniqueidentifier] NOT NULL,
    [CreatedAt] [datetime]         NOT NULL,
    [Title]     [nvarchar](128)    NOT NULL,
    [Content]   [nvarchar](max)    NOT NULL,
    [Version]   [int]              NOT NULL,
    [Status]    [int]              NOT NULL,
    [IsDeleted] [bit]              NOT NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Snapshots]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Snapshots]
(
    [EntityId]      [uniqueidentifier] NOT NULL,
    [EntityVersion] [int]              NOT NULL,
    [Type]          [nvarchar](512)    NOT NULL,
    [Data]          [nvarchar](max)    NOT NULL,
    CONSTRAINT [Snapshots_pk] PRIMARY KEY NONCLUSTERED
        (
         [EntityId] ASC,
         [EntityVersion] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spaces]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spaces]
(
    [Id]         [uniqueidentifier] NOT NULL,
    [ParentId]   [uniqueidentifier] NULL,
    [CreatedAt]  [datetime]         NOT NULL,
    [AuthorId]   [uniqueidentifier] NOT NULL,
    [Name]       [nvarchar](50)     NOT NULL,
    [Color]      [nvarchar](50)     NULL,
    [Icon]       [nvarchar](100)    NULL,
    [Type]       [int]              NOT NULL,
    [Visibility] [int]              NOT NULL,
    [IsDeleted]  [bit]              NOT NULL,
    CONSTRAINT [PK_Spaces] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021/7/31 22:57:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users]
(
    [Id]           [uniqueidentifier] NOT NULL,
    [RegisteredAt] [datetime]         NOT NULL,
    [Email]        [nvarchar](256)    NOT NULL,
    [Password]     [nvarchar](max)    NOT NULL,
    [FirstName]    [nvarchar](32)     NOT NULL,
    [LastName]     [nvarchar](32)     NOT NULL,
    [Bio]          [nvarchar](128)    NULL,
    [Avatar]       [nvarchar](512)    NULL,
    [IsActive]     [bit]              NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Attachments]
(
    [Id]         [uniqueidentifier] NOT NULL,
    [UploadedAt] [datetime]         NOT NULL,
    [UploadedBy] [uniqueidentifier] NOT NULL,
    [Name]       [nvarchar](128)    NOT NULL,
    [Path]       [nvarchar](512)    NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO