export const loadActors = async (context)=>{
    const response = await fetch("https://localhost:44380/actors");
    const responseData = await response.json();
    if(!response.ok){
        throw new Error("unable to fetch actors" || responseData.message);
    }
    console.log(responseData);
    context.commit("setActors",responseData);
}
export const addActor = async(context,data) =>{

    const newActor = {
            Name:data.Name,
            Bio:data.Bio,
            DOB:data.DOB,
            Gender:data.Gender
        
    }
    const response = await fetch('https://localhost:44380/actors',{
        method: 'POST',
        body: JSON.stringify(newActor),
        headers: {
            "Content-Type": "application/json",
          }
    });
    const responseData = await response.json();
    if(!response.ok){
        throw new Error("Unable add actors");
    }
    context.commit('addActor', newActor);
    return responseData.id;
}
