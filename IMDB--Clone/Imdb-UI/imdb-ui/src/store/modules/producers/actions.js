export const loadProducers = async (context)=>{
    const response = await fetch("https://localhost:44380/producers");
    const responseData = await response.json();
    if(!response.ok){
        throw new Error("unable to fetch actors" || responseData.message);
    }
    context.commit("setProducers",responseData);
}
export const addProducer = async(context,data) =>{

    const newProducer = {
            Name:data.Name,
            Bio:data.Bio,
            DOB:data.DOB,
            Gender:data.Gender
        
    }
    const response = await fetch('https://localhost:44380/producers',{
        method: 'POST',
        body: JSON.stringify(newProducer),
        headers: {
            "Content-Type": "application/json",
          }
    });
    const responseData = await response.json();
    if(!response.ok){
        throw new Error("Unable to add producer");
    }
    context.commit('addActor',newProducer);
    return responseData.id;
}