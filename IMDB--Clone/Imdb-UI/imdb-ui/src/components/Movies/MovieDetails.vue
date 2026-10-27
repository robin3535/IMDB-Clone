<template>
    <v-card class="detailContainer">
        <v-img class="contentImg" :src="coverImage" height="50%">
            <v-btn color="primary" fab small dark @click="closeDialog">
                <v-icon>mdi-close</v-icon>
            </v-btn>
        </v-img>
        <v-card class="contentTxt" height="50%">
            <div class="titleContainer">
                <h1>{{ title }}</h1>
                <v-chip-group active-class="deep-purple accent-4 white--text" column>
                    <v-chip v-for="genre in genres" :key="genre">{{ genre }}</v-chip>
                </v-chip-group>
            </div>
            <v-card-text>
                <h2>{{ yor }}</h2>
                <h4>{{ plot }}</h4>
                <h2>Casts: {{ actors.toString() }}</h2>
                <h2>Producer: {{ producer }}</h2>
            </v-card-text>            
        </v-card>
    </v-card>
</template>

<script>
export default {
    emits:['close-dialog'],
    props: ['title', 'genres', 'actors', 'yor', 'plot', 'producer','coverImage'],
    data() {
        return {
            selection: ''
        }
    },
    computed: {
        actorName() {
            var names = [];
            for (var a in this.actors) {
                names.push(a.Name);
            }
            return names.toString();
        }
    },
    methods:{
        closeDialog(){
            this.$emit('close-dialog');
        }
    }

}
</script>

<style scoped>
.titleContainer {
    display: flex;
    justify-content: space-between;
    padding: 0px 5px 0px 10px;
}

.detailContainer {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

.contentImg {
    flex: 500px;
    padding: 20px;

}
.contentTxt {
    flex: 200px;
    padding: 5px;
    font-size: 30px;
}
h2,h4{
    padding-top: 6px;
}

</style>