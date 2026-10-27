export const loadGenres = async (context)=>{
    const response = await fetch("https://localhost:44380/genres");
    const responseData = await response.json();
    if(!response.ok){
        throw new Error("unable to fetch actors" || responseData.message);
    }
    console.log(responseData);
    context.commit("setGenres", responseData);
}
