using StreamTV.Context;
using StreamTV.Interfaces;
using StreamTV.Models;
using System;

namespace StreamTV.Application
{
    public class VideosApplication : BaseApplication<Videos>, IVideosRepository
    {
        private DatabaseContext _context;
        public VideosApplication(DatabaseContext context)
        {
            _context = context;
        }

        public bool InsertConfirm(Videos video)
        {
            try
            {
                _context.Add(video);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
