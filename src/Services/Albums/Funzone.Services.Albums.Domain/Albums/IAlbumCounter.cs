﻿using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Albums
{
    public interface IAlbumCounter
    {
        int CountAlbumsWithUserId(UserId userId);
        int CountPicturesWithAlbumId(AlbumId albumId);
    }
}