export const setActors = (state,payload) =>{
    state.actors = payload;
}
export const addActor = (state,payload) => {
    console.log(payload)
    state.actors.push = payload;
}