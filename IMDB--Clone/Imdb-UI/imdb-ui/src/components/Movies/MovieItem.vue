<template>
    <div>
        <v-card class="mx-auto" max-width="350" min-width="200">
            <!-- <v-img src="https://cdn.vuetifyjs.com/images/cards/docks.jpg" height="200px"></v-img> -->
            <v-img :src="coverImage" min-height="100px" max-height="370"></v-img>
            <v-card-title class="title">
                {{ name }}
            </v-card-title>

            <v-card-subtitle>
                {{ shortPlot }}
            </v-card-subtitle>
                <v-row>
                    <v-col>
                        <v-btn color="rgb(0,0,255)" text @click="onExplore"> Explore
                        <v-icon>mdi-arrow-right</v-icon>
                    </v-btn>
                    </v-col>
                   
                    <v-spacer></v-spacer>
                    <v-col>
                        <v-btn icon color="rgb(0,0,255)" @click="onEdit">
                        <v-icon>mdi-movie-open-edit-outline</v-icon>
                    </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn icon color="rgb(255,0,0)" @click="onDelete">
                        <v-icon>mdi-delete-outline</v-icon>
                    </v-btn>
                    </v-col>  
                </v-row>
            
        </v-card>
    </div>
</template>
<script>
export default {
    emits: ['edit', 'delete', 'explore'],
    props: ['id', 'name', 'coverImage', 'plot'],
    methods: {
        onExplore() {
            this.$emit('explore', this.id);
        },
        onEdit() {
            this.$emit('edit', this.id);
        },
        onDelete() {
            this.$emit('delete', this.id);
        }
    },computed:{
        shortPlot(){
            var array = this.plot.split(" ");
            if(array.length > 30){
                var str;
                for(var i=0;i<30;i++){
                    str += array[i] + " ";
                }
                return str + "..."
            }else{
                return this.plot
            }
        }
    }
}
</script>

<style scoped>
.title {
    font-weight: bold;
}
</style>
