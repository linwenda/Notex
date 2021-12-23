/****** Object:  Table [dbo].[Attachments]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments]
(
    [Id]          [uniqueidentifier] NOT NULL,
    [UploadedAt]  [datetime]         NOT NULL,
    [UploaderId]  [uniqueidentifier] NOT NULL,
    [DisplayName] [nvarchar](128)    NOT NULL,
    [StoredName]  [nvarchar](256)    NOT NULL,
    [Path]        [nvarchar](512)    NOT NULL,
    [ContentType] [nvarchar](128)    NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventStore]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventStore]
(
    [AggregateId]      [uniqueidentifier] NOT NULL,
    [AggregateVersion] [int]              NOT NULL,
    [Timestamp]        [datetime]         NOT NULL,
    [Type]             [nvarchar](max)    NOT NULL,
    [Data]             [nvarchar](max)    NOT NULL,
    CONSTRAINT [EventStore_pk] PRIMARY KEY NONCLUSTERED
        (
         [AggregateId] ASC,
         [AggregateVersion] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteComments]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteComments]
(
    [Id]               [uniqueidentifier]  NOT NULL,
    [CreationTime]     [datetimeoffset](7) NOT NULL,
    [NoteId]           [uniqueidentifier]  NOT NULL,
    [AuthorId]         [uniqueidentifier]  NOT NULL,
    [ReplyToCommentId] [uniqueidentifier]  NULL,
    [Content]          [nvarchar](512)     NOT NULL,
    [IsDeleted]        [bit]               NOT NULL,
    CONSTRAINT [PK_NoteComments] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCooperations]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCooperations]
(
    [Id]           [uniqueidentifier]  NOT NULL,
    [CreationTime] [datetimeoffset](7) NOT NULL,
    [NoteId]       [uniqueidentifier]  NOT NULL,
    [SubmitterId]  [uniqueidentifier]  NOT NULL,
    [AuditorId]    [uniqueidentifier]  NULL,
    [AuditTime]    [datetime]          NULL,
    [Comment]      [nvarchar](256)     NOT NULL,
    [RejectReason] [nvarchar](256)     NULL,
    [Status]       [int]               NOT NULL,
    CONSTRAINT [PK_Cooperations] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteHistories]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteHistories]
(
    [Id]           [uniqueidentifier]  NOT NULL,
    [NoteId]       [uniqueidentifier]  NULL,
    [AuthorId]     [uniqueidentifier]  NULL,
    [CreationTime] [datetimeoffset](7) NULL,
    [Title]        [nvarchar](128)     NULL,
    [Blocks]       [nvarchar](max)     NULL,
    [Version]      [int]               NULL,
    [Comment]      [nvarchar](256)     NULL,
    CONSTRAINT [PK_NoteHistories] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteMembers]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteMembers]
(
    [NoteId]   [uniqueidentifier] NOT NULL,
    [MemberId] [uniqueidentifier] NOT NULL,
    [Role]     [nvarchar](64)     NOT NULL,
    [JoinTime] [datetime]         NOT NULL,
    [IsActive] [bit]              NOT NULL,
    [LeaveAt]  [datetime]         NULL,
    CONSTRAINT [PK_NoteMembers] PRIMARY KEY CLUSTERED
        (
         [NoteId] ASC,
         [MemberId] ASC,
         [JoinTime] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteMergeRequests]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteMergeRequests]
(
    [Id]           [uniqueidentifier]  NOT NULL,
    [CreatorId]    [uniqueidentifier]  NOT NULL,
    [CreationTime] [datetimeoffset](7) NOT NULL,
    [NoteId]       [uniqueidentifier]  NOT NULL,
    [ReviewerId]   [uniqueidentifier]  NULL,
    [ReviewTime]   [uniqueidentifier]  NULL,
    [Title]        [nvarchar](128)     NOT NULL,
    [Description]  [nvarchar](512)     NULL,
    [Status]       [int]               NOT NULL,
    CONSTRAINT [PK_NoteMergeRequests] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes]
(
    [Id]           [uniqueidentifier]  NOT NULL,
    [ForkId]       [uniqueidentifier]  NULL,
    [AuthorId]     [uniqueidentifier]  NOT NULL,
    [SpaceId]      [uniqueidentifier]  NOT NULL,
    [CreationTime] [datetimeoffset](7) NOT NULL,
    [Title]        [nvarchar](128)     NOT NULL,
    [Blocks]       [nvarchar](max)     NOT NULL,
    [Version]      [int]               NOT NULL,
    [Status]       [int]               NOT NULL,
    [IsDeleted]    [bit]               NOT NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Snapshots]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Snapshots]
(
    [AggregateId]      [uniqueidentifier] NOT NULL,
    [AggregateVersion] [int]              NOT NULL,
    [Type]             [nvarchar](512)    NOT NULL,
    [Data]             [nvarchar](max)    NOT NULL,
    CONSTRAINT [Snapshots_pk] PRIMARY KEY NONCLUSTERED
        (
         [AggregateId] ASC,
         [AggregateVersion] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spaces]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spaces]
(
    [Id]                [uniqueidentifier]  NOT NULL,
    [ParentId]          [uniqueidentifier]  NULL,
    [CreationTime]      [datetimeoffset](7) NOT NULL,
    [AuthorId]          [uniqueidentifier]  NOT NULL,
    [Name]              [nvarchar](64)      NOT NULL,
    [BackgroundColor]   [nvarchar](64)      NULL,
    [BackgroundImageId] [uniqueidentifier]  NULL,
    [Type]              [int]               NOT NULL,
    [Visibility]        [int]               NOT NULL,
    [IsDeleted]         [bit]               NOT NULL,
    CONSTRAINT [PK_Spaces] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021/11/11 23:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users]
(
    [Id]           [uniqueidentifier]  NOT NULL,
    [CreationTime] [datetimeoffset](7) NOT NULL,
    [Email]        [nvarchar](256)     NOT NULL,
    [Password]     [nvarchar](max)     NOT NULL,
    [FirstName]    [nvarchar](32)      NOT NULL,
    [LastName]     [nvarchar](32)      NOT NULL,
    [Bio]          [nvarchar](128)     NULL,
    [Avatar]       [nvarchar](512)     NULL,
    [IsActive]     [bit]               NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
