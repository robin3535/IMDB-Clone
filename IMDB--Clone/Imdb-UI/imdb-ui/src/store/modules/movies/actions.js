export const loadMovies = async(context)=>{
    const response = await fetch('https://localhost:44380/movies');
    const responseData = await response.json();
    if(!response.ok){
        throw new Error('Unable to fetch' || responseData.message);
    }
    console.log(responseData);
    context.commit('setMovies',responseData);
}

export const addMovie = async(context,data) =>{
    const newMovie = {
            Name:data.name,
            Plot:data.plot,
            Yor:data.yor,
            Actors:data.actors.toString(),
            Genres:data.genres.toString(),
            Profit:data.profit,
            Language:data.language,
            ProducerId:data.producer,
            CoverImage:data.coverImage
        
    }
    try {
        var coverImageUrl = await uploadCoverImage({file:data.file,name:data.name});
    } catch (error) {
        console.log(error.message);
        
    }
    newMovie.CoverImage = coverImageUrl;
    data.coverImage = coverImageUrl;
    const response = await fetch('https://localhost:44380/movies',{
        method: 'POST',
        body: JSON.stringify(newMovie),
        headers: {
            "Content-Type": "application/json",
          }
    });
    var responseData = await response.json();
    if(!response.ok){
        throw new Error("Unable to add movie");
    }
    console.log()
    context.commit('addMovie',{id:responseData.id,...data});
}
export const updateMovie = async(context,data) =>{
    const newMovie = {
            Name:data.name,
            Plot:data.plot,
            Yor:data.yor,
            Actors:data.actors.toString(),
            Genres:data.genres.toString(),
            Profit:data.profit,
            Language:data.language,
            ProducerId:data.producer,
            CoverImage:data.coverImage
    }
    const response = await fetch(`https://localhost:44380/movies/${data.id}`,{
        method: 'PUT',
        body: JSON.stringify(newMovie),
        headers: {
            "Content-Type": "application/json",
          }
    });
    if(!response.ok){
        //error
    }
    context.commit('updateMovie',data);
}
export const deleteMovie = async(context,id) =>{

    const response = await fetch(`https://localhost:44380/movies/${id}`,{
        method: 'DELETE'
    });
    if(!response.ok){
        throw new Error("Something went wrong");
    }
    context.commit('deleteMovie',id);
}
const uploadCoverImage = async(payload) =>{
    var data =  new FormData();
    data.append('file',payload.file);
    const response = await fetch(`https://localhost:44380/movies/upload/${payload.name}`,{
        method:'POST',
        body:data     
    });
    if(!response.ok){
        throw new Error("Something went wrong");
    }
    var responseData = await response.text();
    return responseData;
}
export const retrieveCoverImage = async(context,payload)=>{
    const response = await fetch(payload);
    if(!response.ok){
        throw new Error("Unable to fetch");
    }
    const blobImage = await response.blob();

    context.commit('retrieveCoverImage',blobImage);
}