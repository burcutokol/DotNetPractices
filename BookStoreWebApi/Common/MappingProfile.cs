using AutoMapper;
using BookStoreWebApi.Application.AuthorOperations.Commands.CreateAuthor;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreWebApi.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreWebApi.Application.GenreOperations.Commands.UpdateGenre;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreWebApi.Application.GenreOperations.Queries.GetGenres;
using BookStoreWebApi.BookOperations.CreateBook;
using BookStoreWebApi.BookOperations.GetBookDetail;
using BookStoreWebApi.BookOperations.GetBooks;
using BookStoreWebApi.Entities;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    { 
        public MappingProfile() 
        {
            CreateMap<CreateBookModel, Book>(); //first arg is source, 2nd is destination. createbookmodel try to convert book
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(x => x.AuthorName, opt => opt.MapFrom(x => x.Author.Name)).ForMember(x => x.AuthorSurname, opt => opt.MapFrom(x => x.Author.Surname));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(x => x.AuthorName, opt => opt.MapFrom(x => x.Author.Name)).ForMember(x => x.AuthorSurname, opt => opt.MapFrom(x => x.Author.Surname));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<UpdateGenreModel, Genre>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Author, AuthorDetailModel>();
            CreateMap<CreateAuthorModel, Author>();

        } 
    }
}
