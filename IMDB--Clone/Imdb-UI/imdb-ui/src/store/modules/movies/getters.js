export const movies = (state) =>{
    return state.movies;
}
export const hasMovies = (state) =>{
    return state.movies && state.movies.length > 0;
}
export const coverImage = (state) =>{
    return state.coverImageEdit;
}