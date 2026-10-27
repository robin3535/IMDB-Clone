<template>
    <v-card>
        <v-card-title pb-5>
            <h2>{{ title }}</h2>
        </v-card-title>
        <v-card-text>
            <v-form ref="castform" v-model="isValidForm" lazy-validation @submit.prevent>
                <v-row>
                    <v-col>
                        <v-text-field label="Name" v-model="name" solo type="string" required
                            :rules="[v => !!v || 'Name is required']">
                        </v-text-field>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field label="Date of Birth" v-model="dob" solo type="date" required
                            :rules="[v => !!v || 'DOB is required']">
                        </v-text-field>
                    </v-col>
                </v-row>
                <v-radio-group label="Gender" v-model="gender" row gender required
                    :rules="[v => !!v || 'Gender is required']">
                    <v-radio label="Male" value="Male"></v-radio>
                    <v-radio label="Female" value="Female"></v-radio>
                </v-radio-group>
                <v-row>
                    <v-col cols="12">
                        <v-textarea v-model="bio" label="Bio" solo type="string" required
                            :rules="[v => !!v || 'Bio is required']"></v-textarea>
                    </v-col>
                </v-row>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="rgb(200,10,0)" width="80px" @click="closeForm" class="btn" text>
                        Close
                    </v-btn>
                    <v-btn color="rgb(0,0,255)" width="80px" class="btn" id="add-cast" @click="submitForm" text>
                        Save
                    </v-btn>
                </v-card-actions>
            </v-form>
        </v-card-text>
    </v-card>
</template>
<script>
export default {
    emits: ['add-cast', 'close-dialog'],
    props: ['title'],
    data() {
        return {
            isValidForm: true,
            name: '',
            dob: '',
            bio: '',
            gender: null
        }
    },
    methods: {
        submitForm() {
            var castFormData = {
                Name: this.name,
                DOB: this.dob,
                Bio: this.bio,
                Gender: this.gender
            };
            if (this.$refs.castform.validate()) {
                this.$emit('add-cast', castFormData);
                this.$refs.castform.reset();
            }

        },
        closeForm() {
            this.$refs.castform.reset();
            this.$emit('close-dialog');
        }
    }
}
</script>

<style scoped>
.btn {
    margin: 0px 0px 0px 4px;
    font-weight: bold;
}

h2 {
    color: rgb(26, 98, 157);
    padding: 10px 0px 5px 5px;
}
</style>