create table EventStore
(
    AggregateId      uniqueidentifier not null,
    AggregateVersion int              not null,
    Timestamp        datetime         not null,
    Type             nvarchar(max)    not null,
    Data             nvarchar(max)    not null,
    constraint EventStore_pk
        primary key nonclustered (AggregateId, AggregateVersion)
)
go

create table Snapshots
(
    AggregateId      uniqueidentifier not null,
    AggregateVersion int              not null,
    Type             nvarchar(512)    not null,
    Data             nvarchar(max)    not null,
    constraint Snapshots_pk
        primary key nonclustered (AggregateId, AggregateVersion)
)
go

create table NoteCooperations
(
    Id           uniqueidentifier not null
        constraint PK_Cooperations
            primary key,
    NoteId       uniqueidentifier not null,
    SubmitterId  uniqueidentifier not null,
    SubmittedAt  datetime         not null,
    AuditorId    uniqueidentifier,
    AuditedAt    datetime,
    Comment      nvarchar(256)    not null,
    RejectReason nvarchar(256),
    Status       int              not null
)
go

create table NoteHistories
(
    Id        uniqueidentifier not null
        constraint PK_NoteHistories
            primary key,
    NoteId    uniqueidentifier,
    AuthorId  uniqueidentifier,
    AuditedAt datetime,
    Title     nvarchar(128),
    Content   nvarchar(max),
    Version   int,
    Comment   nvarchar(256)
)
go

create table NoteMembers
(
    NoteId   uniqueidentifier not null,
    MemberId uniqueidentifier not null,
    Role     nvarchar(50)     not null,
    JoinedAt datetime         not null,
    IsActive bit              not null,
    LeaveAt  datetime,
    constraint PK_NoteMembers
        primary key (NoteId, MemberId, JoinedAt)
)
go

create table Notes
(
    Id        uniqueidentifier not null
        constraint PK_Notes
            primary key,
    FromId    uniqueidentifier null,
    AuthorId  uniqueidentifier not null,
    CreatedAt datetime         not null,
    Title     nvarchar(128)    not null,
    Content   nvarchar(max)    not null,
    Version   int              not null,
    Status    int              not null,
    IsDeleted bit              not null
)
go

create table Users
(
    Id           uniqueidentifier not null
        constraint PK_Users
            primary key,
    RegisteredAt datetime         not null,
    Email        nvarchar(256)    not null,
    NickName     nvarchar(128),
    Password     nvarchar(max)    not null,
    IsActive     bit              not null
)
go

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

CREATE TABLE [dbo].[Spaces]
(
    [Id]        [uniqueidentifier] NOT NULL,
    [CreatedAt] [datetime]         NOT NULL,
    [AuthorId]  [uniqueidentifier] NOT NULL,
    [Name]      [nvarchar](50)     NOT NULL,
    [Color]     [nvarchar](50)     NOT NULL,
    [Icon]      [nvarchar](100)    NULL,
    [IsDeleted] [bit]              NOT NULL,
    CONSTRAINT [PK_Spaces] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SpaceFolders]
(
    [Id]        [uniqueidentifier] NOT NULL,
    [SpaceId]   [uniqueidentifier] NOT NULL,
    [AuthorId]  [uniqueidentifier] NOT NULL,
    [ParentId]  [uniqueidentifier] NULL,
    [CreatedAt] [datetime]         NOT NULL,
    [Name]      [nvarchar](50)     NOT NULL,
    CONSTRAINT [PK_SpaceFolders] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO