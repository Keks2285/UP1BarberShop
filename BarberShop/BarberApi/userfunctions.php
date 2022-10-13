<?php
    function authorization($connect, $data){

        $searchUser=$connect->prepare("select * from Employe join Post where Post_ID=ID_Post and Email=? and Password=?");
        $searchUser->execute(array(strval($data["email"]), strval($data["password"]) ));
        $listUser=$searchUser->fetchAll();

        //тут для клиета тоже надо будет сделать
        //$searchUser=$connect->prepare("select * from `Employee` join Studio where Studio_ID=ID_Studio and ID_Anime=?");
        //$searchUser->execute(array($data));
        //$listUser=$searchUser->fetchAll();
           // echo print_r($listUser); die();
        if(count($listUser)==0){
            $responce=[
                "status"=>false,
                "message"=>"user not found!"
            ];
            http_response_code(404);
        }
        else{
            $responce=[
                "status"=>true,
                "message"=>"Authorizated!"
            ];
            http_response_code(200);
        }
        echo json_encode($responce);
    }
