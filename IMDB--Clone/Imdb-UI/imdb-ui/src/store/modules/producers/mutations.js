export const setProducers = (state,payload) =>{
    state.producers = payload;
}
export const addProducer = (state,payload) => {
    console.log(payload)
    state.producers.push = payload;
}