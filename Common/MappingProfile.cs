using AutoMapper;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using System.Collections.Generic;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    { 
        public MappingProfile() 
        {
            CreateMap<CreateBookModel, Book>(); //first arg is source, 2nd is destination. createbookmodel try to convert book
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        } 
    }
}
