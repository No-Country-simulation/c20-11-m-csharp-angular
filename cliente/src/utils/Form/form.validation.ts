const valid_image_url = /https?:\/\/.*\.(?:png|jpg|jpeg|gif|bmp|webp|svg)(\?.*)?$/;

interface Dicctionary<T>{
    [Key:string] : T
}

const ImageUrlValidate = (value:string) => {
    if(valid_image_url.test(value)) return true;
    return "El URL de la imagen debe ser valido";
}

const DescriptionValidate = (value:string) => {
    if(value.length < 700 && value.length > 25) return true;
    return "La descripcion debe tener menos de 700 caracteres y mas de 25 caracteres";
}

const NombreValidate = (value:string) => {
    if(value.length < 30 && value.length > 1) return true;
    return "El Nombre debe tener menos de 30 caracteres y mas de 1 caracter";
}
const TiempoCoccionValidate = (value:string) => {
    if(!Number.isNaN(parseFloat(value))) return true;
    return "El Tiempo de coccion debe ser un numero";
}
const ValidateActions:Dicctionary<(value:any) => true|string> = {
    imageUrl:ImageUrlValidate,
    descripcion:DescriptionValidate,
    nombre:NombreValidate,
    tiempo_de_coccion:
}

export const ValidateForm = (event:HTMLInputElement) => {

    const {name,value} =event;

    
    if(ValidateActions[name]){
        console.log(name,value);
        return ValidateActions[name](value);
    }else{
        return false;
    }

}

