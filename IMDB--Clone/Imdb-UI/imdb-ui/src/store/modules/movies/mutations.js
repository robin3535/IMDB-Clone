export const deleteMovie = (state,id) =>{
    var filteredMovies = state.movies.filter((m) => m.id != id);
    state.movies = filteredMovies;
}
export const updateMovie =(state,payload)=>{
    var filteredMovies = state.movies.filter((m) => m.id != payload.id);
    filteredMovies.push(payload);
    state.movies = filteredMovies;
}
export const addMovie =(state,payload)=>{
    state.movies.push(payload);
    console.log(payload)
    return payload.id;
}
export const setMovies = (state,payload) =>{
    state.movies = payload;
}
export const uploadImage = (state,payload) =>{
    var movie = state.movies.find(m => m.id == payload.id)
    movie.coverImage = payload.responseData;
    var filteredMovies = state.movies.filter((m) => m.id != payload.id);
    filteredMovies.push(movie);
    state.movies = filteredMovies;
}
export const retrieveCoverImage = (state,payload) =>{
    console.log(payload);
    state.coverImageEdit = payload;
}